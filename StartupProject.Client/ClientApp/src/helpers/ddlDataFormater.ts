export const ddlDataFormater = (data: any) => {
  let list:any = [];
  data &&
    data.forEach((d: any) => {
      list.push({ ...d, label: d.Text || d.Name, value: d.Value || d.Id });
    });
  return list;
};
