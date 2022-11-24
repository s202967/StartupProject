import types from "../types";
import { Action, Reducer } from "redux";

export interface InitialState {
  messages: object;
  otherMessage: object;
  arrayMessages?: any;
}

const initialState: InitialState = {
  messages: {},
  otherMessage: {},
  arrayMessages: [],
};

export default function (state = initialState, action: any) {
  switch (action.type) {
    case types.ADD_MESSAGE:
      return {
        ...state,
        messages: action.payload,
      };

    case types.ADD_MESSAGES:
      return {
        ...state,
        arrayMessages: [action.payload, ...state.arrayMessages],
      };

    case types.ADD_OTHER_MESSAGES:
      return {
        ...state,
        otherMessage: action.payload,
      };

    case types.REMOVE_MESSAGE:
      return {
        ...state,
        arrayMessages: state.arrayMessages.filter(
          (message: any) => message.id !== action.payload
        ),
      };

    case types.CLEAR_MESSAGE:
      return {
        ...state,
        messages: {},
      };

    default:
      return state;
  }
}
