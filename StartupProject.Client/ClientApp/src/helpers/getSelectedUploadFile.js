import { validUploadDocExtension } from "../constants/validExtensions";

export const getSelectedUploadFile = (selectedFiles, prevSelectedFiles) => {
  const files = [];
  const fileList = selectedFiles;
  let inValidFileExists = false;
  for (var i = 0; i < fileList ? fileList.length : 0; i++) {
    const file = fileList.item(i);

    Object.defineProperty(file, "myProp", {
      value: true,
    });
    let splittedFileName = file.name.split(".");
    if (
      !validUploadDocExtension.includes(
        splittedFileName[splittedFileName.length - 1]
      )
    ) {
      inValidFileExists = true;
      break;
    }
    files.push(file);
  }

  const nonRepeatingFiles =
    prevSelectedFiles &&
    prevSelectedFiles.filter(
      ({ name: id1 }) => !files && files.some(({ name: id2 }) => id2 === id1)
    );
  var mergedFiles = files.concat(nonRepeatingFiles);
  return { inValidFileExists, mergedFiles };
};
