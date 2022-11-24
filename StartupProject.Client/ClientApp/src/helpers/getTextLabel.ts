interface dataProps {
  Text?: string;
  Value: string;
  Key?: string | number;
}

interface Label {
  label: string;
  value: string | number;
}

export const Arrays = (data: [dataProps]) => {
  let arrayItem: any = [];
  if (data && Array.isArray(data)) {
    data.map((item, key) => {
      arrayItem.push({ label: item.Text, value: item.Value });
    });
  }
  return arrayItem;
};

export const ArraysKey = (data: [dataProps]) => {
  let arrayItem: any = [];
  if (data && Array.isArray(data)) {
    data.map((item, key) => {
      arrayItem.push({ label: item.Value, value: item.Key });
    });
  }
  return arrayItem;
};

type EmployeeLabel = {
  FullName: string;
  EmployeeId: string | number;
  Imagepath: string;
};

export const ArraysWithImage = (data: [EmployeeLabel]) => {
  let arrayItem: any = [];
  if (data) {
    data.map((item, key) => {
      item &&
        arrayItem.push({
          label: item.FullName,
          value: item.EmployeeId,
          img: item.Imagepath,
        });
    });
  }
  return arrayItem;
};
