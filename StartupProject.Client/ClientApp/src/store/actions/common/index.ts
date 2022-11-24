import { post, get, api } from "networkService";
import types from "store/types";

export const GetRoles = () => async (dispatch: Function) => {
  const res: any = await get(api.common.roles, dispatch);
  dispatch({ type: types.GET_ROLES, payload: res.Data });
};
