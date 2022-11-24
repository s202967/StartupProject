import { post, get, api } from "networkService";
import types from "store/types";

export const GetTemplates = () => async (dispatch: Function) => {
  const res: any = await get(api.meta.templates, dispatch);
  console.log(res);
  dispatch({ type: types.GET_TEMPLATES, payload: res });
};

export const GetCheckList = () => async (dispatch: Function) => {
  const res: any = await get(api.meta.checkList, dispatch);
  dispatch({ type: types.GET_CHECKLIST, payload: res });
};

export const GetSection = () => async (dispatch: Function) => {
  const res: any = await get(api.meta.sections, dispatch);
  dispatch({ type: types.GET_SECTIONS, payload: res });
};
export const GetComponents = () => async (dispatch: Function) => {
    const res: any = await get(api.meta.components, dispatch);
    dispatch({ type: types.GET_COMPONENTS, payload: res });
  };

