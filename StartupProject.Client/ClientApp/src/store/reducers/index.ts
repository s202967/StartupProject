import { combineReducers } from "redux";
import commonReducer from "./common/commonReducer";
import validateReducer from "./validateReducer";
import userReducer from "./userReducer";
import metaReducer from "./metaReducer";
export default combineReducers({
  commonReducer,
  validateReducer,
  userReducer,
  metaReducer,
});
