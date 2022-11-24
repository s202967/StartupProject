import types from "../types";

const initialState = {
  checkList: [],
  sections: [],
  templates: [],
  components: [],
};

export default function (state: any = initialState, action: any) {
  switch (action.type) {
    case types.GET_CHECKLIST:
      return {
        ...state,
        checkList: action.payload,
      };
    case types.GET_SECTIONS:
      return {
        ...state,
        sections: action.payload,
      };
    case types.GET_TEMPLATES:
      return {
        ...state,
        templates: action.payload,
      };
    case types.GET_COMPONENTS:
      return {
        ...state,
        components: action.payload,
      };

    default:
      return state;
  }
}
