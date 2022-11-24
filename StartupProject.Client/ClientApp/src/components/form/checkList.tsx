import React, { useState } from "react";
import { Form, Input, Button, Select, Checkbox } from "element-react";

const CheckList = (props) => {
  const { list, onChange, value } = props;

  const handleCheckBoxChange = (item, isChecked) => {
    let newState = [...value];
    var index = newState.findIndex((x) => x.Id === item.Id);
    if (isChecked) {
      newState.push(item);
    } else if (index > -1) {
      newState.splice(index);
    }
    onChange(newState);
  };
  const containsItems = (item) => {
    var len = value.filter((x) => x.Id === item.Id).length;
    return len > 0;
  };
  return (
    <div className="ctrl-check-list">
      {list &&
        list.map((item, index) => (
          <div>
            <Checkbox
              key={index}
              label={item.Name}
              checked={containsItems(item)}
              onChange={(e) => handleCheckBoxChange(item, e)}
            ></Checkbox>
          </div>
        ))}
    </div>
  );
};

export default CheckList;
