import types from "../types";

interface dispatchAction {
  type: string;
  payload: object | string | [];
}

export const toastMessage = (dispatch: any, res: any) => {
  let MessageType = null;
  let Message = [];

  if (res) {
    if (res.MessageType) {
      MessageType = res.MessageType;
    } else if (res.data && res.data.MessageType) {
      MessageType = res.data.MessageType;
    }

    if (res.Message) {
      Message = res.Message;
    } else if (res.data && res.data.Message) {
      Message = res.data.Message;
    }
  }

  if (MessageType || Message) {
    dispatch({
      type: types.ADD_MESSAGE,
      payload: { type: MessageType, message: Message },
    });
  }
};

export const clearMessage =
  () => (dispatch: (action: dispatchAction) => dispatchAction) => {
    dispatch({
      type: types.CLEAR_MESSAGE,
      payload: {},
    });
  };
