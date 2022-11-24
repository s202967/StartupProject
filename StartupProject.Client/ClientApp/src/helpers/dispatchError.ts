import React from "react";
import { toastMessage } from "../store/actions/validateAction";

export function dispatchError(dispatch: Function, error: any) {
  let response: any = {};
  response.MessageType = error.MessageType || error.messageType || "Warning";
  response.Message = error.Message || error.message || error;

  dispatch && toastMessage(dispatch, response);
}
