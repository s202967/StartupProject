import { post, get, api } from "networkService";
import types from "store/types";

export const GetLoggedInUserDetails = () => async (dispatch: Function) => {
  const res = await get(api.users.userDetails, dispatch);
  dispatch({ type: types.GET_LOGGEDIN_USER_DETAIL, payload: res });
};
