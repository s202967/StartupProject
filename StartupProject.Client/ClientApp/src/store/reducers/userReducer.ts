import types from "../types";

import { Action, Reducer } from "redux";
import { UserDetailInterface } from "interfaces/actions/auth";
export interface InitialState {
  loggedInUserDetail: UserDetailInterface;
}

const initialState: InitialState = {
  loggedInUserDetail: {} as UserDetailInterface,
};

const usersReducer: Reducer = (state = initialState, action: any) => {
  switch (action.type) {
    case types.GET_LOGGEDIN_USER_DETAIL:
      return {
        ...state,
        loggedInUserDetail: action.payload,
      };

    default:
      return state;
  }
};
export default usersReducer;
