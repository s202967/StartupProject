export const checkIsFileImageType = (fileName: any) => {
  const splittedFile = fileName ? fileName.split(".") : [];
  const fileType = splittedFile[splittedFile && splittedFile.length - 1];
  const imageTypes = ["gif", "jpeg", "png", "jpg"];
  return imageTypes.includes(fileType);
};
