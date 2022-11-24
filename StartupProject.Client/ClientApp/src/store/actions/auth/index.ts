import { post, api } from "networkService";
import { LoginInterface } from "interfaces/actions/auth";
import Routes from "routes";

const saveToken = (res: any, dispatch: Function) => {
  if (res) {
    //Save to local Storage
    let AuthToken = "bearer " + res.Token;
    let expires_in = res.ExpireMinutes;
    let RefreshToken = res.RefreshToken;
    let IdleTimeoutMinutes = res.IdleTimeoutMinutes;
    //Set token to ls
    localStorage.setItem("AuthToken", AuthToken);
    localStorage.setItem("UserName", res.Username);
    localStorage.setItem("RefreshToken", RefreshToken);
    localStorage.setItem("IdleTimeOut", IdleTimeoutMinutes);
    localStorage.setItem("ExpiresMinutes", expires_in);
  }
};
const clearToken = () => {
  localStorage.setItem("AuthToken", null);
  localStorage.setItem("UserName", null);
  localStorage.setItem("RefreshToken", null);
  localStorage.setItem("IdleTimeOut", null);
  localStorage.setItem("ExpiresMinutes", null);
};
export const login =
  (param: LoginInterface, history: any) => async (dispatch: Function) => {
    let result: any = await post(api.auth.siginIn, dispatch, param).catch(
      (ex) => {
        return false;
      }
    );
    if (result && result.Status) {
      console.log("login success");
      console.log(result);
      saveToken(result.Data, dispatch);
      window.location.href = Routes.submitter.dashboard;
    }
  };

export const logout =
  (param: any, history: any) => async (dipatch: Function) => {
    //clearToken();
    localStorage.clear();
    window.location.href = Routes.login;
  };
