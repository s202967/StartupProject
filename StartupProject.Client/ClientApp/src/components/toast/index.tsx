import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { Notification } from "element-react";
import { clearMessage } from "store/actions/validateAction";


const Toast = (props) => {
  const { messages } = useSelector((state: any) => state.validateReducer);
  const dispatch = useDispatch();
  const timeout = 3000;

  useEffect(() => {
    
    if (messages && messages.message && messages.message.length > 0) {
      messages.message.forEach((message: any) => {
        const type = messages.type.toLowerCase();

        Notification({
          message: message,
          type: type,
          duration: timeout,
          offset: 50,
        });
        setTimeout(() => {
          dispatch(clearMessage());
        }, timeout);
      });
    }
  }, [messages]);

  return <></>;
};
export default Toast;
