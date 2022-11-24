using StartupProject.Core.Caching;
using StartupProject.Core.Infrastructure.DataAccess;
using StartupProject.Core.Infrastructure.Localization;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace StartupProject.Services.Common.Localization
{
    /// <summary>
    /// Provides information about localization
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        private const string LOCALSTRINGRESOURCES_ALL_PUBLIC_KEY = "App.lsr.all.public";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "App.lsr.all";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        private const string LOCALSTRINGRESOURCES_ALL_ADMIN_KEY = "App.lsr.all.admin";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string LOCALSTRINGRESOURCES_PATTERN_KEY = "App.lsr.";

        /// <summary>
        /// Key pattern to split resource by group
        /// </summary>
        private const string ADMIN_LOCALSTRINGRESOURCES_PATTERN = "Admin.";

        #endregion

        #region Fields

        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IHostingEnvironment _env;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Static cache manager</param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="lsrRepository">Locale string resource repository</param>
        public LocalizationService
        (
            IStaticCacheManager cacheManager,
            IHostingEnvironment hostingEnvironment,
            IRepository<LocaleStringResource> lsrRepository
        )
        {
            _cacheManager = cacheManager;
            _lsrRepository = lsrRepository;
            _env = hostingEnvironment;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Insert resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void InsertLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            foreach (var item in resources)
            {
                var obj = new LocaleStringResource
                {
                    ResourceName = item.ResourceName,
                    ResourceValue = item.ResourceValue,
                    //   LanguageId = item.LanguageId
                };
                //item.CreatedBy = userId;
                //item.CreatedDate = DateTime.UtcNow;
                //insert
                _lsrRepository.Add(obj);
                _lsrRepository.Commit();
            }
            ////cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);

            ////event notification
            //foreach (var resource in resources)
            //{
            //    _eventPublisher.EntityInserted(resource);
            //}
        }

        /// <summary>
        /// Update resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void UpdateLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            foreach (var item in resources)
            {

                _lsrRepository.Update(item);
                _lsrRepository.Commit();
            }

            ////cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);
        }

        private static Dictionary<string, KeyValuePair<int, string>> ResourceValuesToDictionary(IEnumerable<LocaleStringResource> locales)
        {
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }
            return dictionary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Delete(localeStringResource);
            _lsrRepository.Commit();
            //cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;

            return _lsrRepository.GetById(localeStringResourceId);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            return GetLocaleStringResourceByName(resourceName);

        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual IList<LocaleStringResource> GetAllResources()
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        select l;
            var locales = query.ToList();
            return locales;
        }

        /// <summary>
        /// Inserts a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Add(localeStringResource);
            _lsrRepository.Commit();
            ////cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);

            //event notification
            //  _eventPublisher.EntityInserted(localeStringResource);
        }

        /// <summary>
        /// Updates the locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Update(localeStringResource);
            _lsrRepository.Commit();
            //cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);

            //event notification
            //   _eventPublisher.EntityUpdated(localeStringResource);
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="loadPublicLocales">A value indicating whether to load data for the public store only (if "false", then for admin area only. If null, then load all locales. We use it for performance optimization of the site startup</param>
        /// <returns>Locale string resources</returns>
        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(bool? loadPublicLocales)
        {
            var key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY);

            //get all locale string resources by language identifier
            if (!loadPublicLocales.HasValue || _cacheManager.IsSet(key))
            {
                var rez = _cacheManager.Get(key, () =>
                {
                    //we use no tracking here for performance optimization
                    //anyway records are loaded only for read-only operations
                    var query = from l in _lsrRepository.TableNoTracking
                                orderby l.ResourceName
                                //  where l.LanguageId == languageId
                                select l;

                    return ResourceValuesToDictionary(query);
                });

                //remove separated resource 
                _cacheManager.Remove(string.Format(LOCALSTRINGRESOURCES_ALL_PUBLIC_KEY));
                _cacheManager.Remove(string.Format(LOCALSTRINGRESOURCES_ALL_ADMIN_KEY));

                return rez;
            }

            //performance optimization of the site startup
            key = string.Format(loadPublicLocales.Value ? LOCALSTRINGRESOURCES_ALL_PUBLIC_KEY : LOCALSTRINGRESOURCES_ALL_ADMIN_KEY);

            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in _lsrRepository.TableNoTracking
                            orderby l.ResourceName
                            where l.ResourceName != null
                            select l;
                query = loadPublicLocales.Value ? query.Where(r => !r.ResourceName.StartsWith(ADMIN_LOCALSTRINGRESOURCES_PATTERN)) : query.Where(r => r.ResourceName.StartsWith(ADMIN_LOCALSTRINGRESOURCES_PATTERN));
                return ResourceValuesToDictionary(query);
            });

        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            //if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            //{
            //load all records (we know they are cached)
            var resources = GetAllResourceValues(!resourceKey.StartsWith(ADMIN_LOCALSTRINGRESOURCES_PATTERN, StringComparison.InvariantCultureIgnoreCase));
            if (resources.ContainsKey(resourceKey))
            {
                result = resources[resourceKey].Value;
            }

            return result;
        }

        public void ImportResourcesFromXml(bool updateExistingResources = true)
        {
            var filePath = Directory.EnumerateFiles(string.Format(_env.ContentRootPath + "/App_Data/Localization/"), "*.xml",
               SearchOption.TopDirectoryOnly);
            foreach (var file in filePath)
            {
                var xml = File.ReadAllText(file);

                if (string.IsNullOrEmpty(xml))
                    return;

                //stored procedures aren't supported
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                var nodes = xmlDoc.SelectNodes(@"//Language/LocaleResource");

                var existingResources = GetAllResources();
                var newResources = new List<LocaleStringResource>();

                foreach (XmlNode node in nodes)
                {
                    var name = node.Attributes["Name"].InnerText.Trim();
                    var value = "";
                    var valueNode = node.SelectSingleNode("Value");
                    if (valueNode != null)
                        value = valueNode.InnerText;

                    if (string.IsNullOrEmpty(name))
                        continue;

                    //do not use "Insert"/"Update" methods because they clear cache
                    //let's bulk insert
                    var resource = existingResources.FirstOrDefault(x => x.ResourceName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                    if (resource != null)
                    {
                        if (updateExistingResources)
                        {
                            resource.ResourceValue = value;
                        }
                    }
                    else
                    {
                        newResources.Add(
                            new LocaleStringResource
                            {
                                ResourceName = name,
                                ResourceValue = value
                            });
                    }
                }
                InsertLocaleStringResources(newResources);
                UpdateLocaleStringResources(existingResources);
            }
            //clear cache
            _cacheManager.Remove(LOCALSTRINGRESOURCES_PATTERN_KEY);
        }

        #endregion
    }
}
