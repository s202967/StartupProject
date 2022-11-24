import React, { useEffect, useState, useRef } from "react";
import { useSelector, useDispatch } from "react-redux";
import { CgAttachment } from "react-icons/cg";

import classnames from "classnames";

const AttachmentButton = (props) => {
  const { name, disabled, isMultiple, maxFiles, value, onChange } = props;
  const dispatch = useDispatch();

  const handleChange = (event) => {
    // if (!showUploadSection()) return;

    const { files } = event.target;
    onChange && onChange(name, files && files[0]);
  };

  return (
    <span style={{ position: "relative" }}>
      <input
        style={{
          position: "absolute",
          height: "100%", // uploadSize ? "100%" : height + "px",
        }}
        className={classnames({
          "genericForm-group__upload-input": true,
          "disable-input": disabled,
        })}
        type="file"
        multiple={isMultiple}
        name={name}
        disabled={disabled}
        onChange={(event) => handleChange && handleChange(event)}
      />
      <CgAttachment />
      <span>{value && value.name}</span>
    </span>
  );
};

export default AttachmentButton;
