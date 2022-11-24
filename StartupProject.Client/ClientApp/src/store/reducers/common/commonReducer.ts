import types from "../../types";

const initialState = {
  roles: [],
};

export default function (state: any = initialState, action: any) {
  switch (action.type) {
    case types.GET_ROLES:
      return {
        ...state,
        roles: action.payload,
      };

    default:
      return state;
  }
}
