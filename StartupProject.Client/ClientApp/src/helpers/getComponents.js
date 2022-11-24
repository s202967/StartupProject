import React from "react";
import classnames from "classnames";

export const ValidationComponent = ({
  error,
  rootClassName,
  errorClassName,
  children,
}) => (
  <div
    className={classnames({
      [rootClassName]: true,
      error: error,
    })}
  >
    {children}
    {error ? <span className={errorClassName}>{error}</span> : null}
  </div>
);
