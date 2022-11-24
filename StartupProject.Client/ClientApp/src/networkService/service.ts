import axios from "axios";
import { baseURL } from "../constants/urls";
import { dispatchError } from "../helpers/dispatchError";

const setHeaders = {
  headers: {
    Authorization: localStorage.getItem("AuthToken"),
    // "bearer CfDJ8OHn1hv6n-NMv3AmmfbePqyAxJhAze807cPCR43tPOWLrSzN_XZHzApKdgP6D_aEz0X0AbV4JqwC02cSWzH-RssFOvP3SJ8MPdyuem6w9NvUHW3vargyX8Ewc6IlNv36McXoFOqia12RXcRLS0pIZU2PJhOKJyAftUwDp-NkTlEhs1PzgPdkJ8pLgbW5ymcE2aRwgEKqIhY2ojeEeUZFwh59fZZwQop9CdfS7hFNbtnIxjdLnUw3ipotCOArWfrlBR3_UqTYOL4A_jStVkai2AAWVAd7lXyv4MFboeZ4kTN8Aj6PVgP7oyxaSAtmGG0p4EiKbtNKdI42Jqh5RKvrc4GJWpobIukZGuGm9AVeVrEeV-R1yltYi6yGMAwqKMriZWT46R03JUsofVi0wlqHj_JHOJhDX8rFh4m25-O62eS5uvQ9q9wRRwXxor3P9UdeRciwxoIca3pBRkxajzJBRLflbDznW84HDSUM69FjegCOHNiTyjRb86cCF061ZwpMgKME6BzyK_gTm0P4DTvpQt4RDsUiJpvZWk-mEvQpX6K2af2EsGcUSJx2bkcbskmjizXKjZ_HhStvPG4cagHbS6J8Ya4M61D2slGbMrKMo4Lq3BnMuVZ5NXbs0AWSDk6hRCTPOjaTrREnnnqbwnHj8mveOuhr1GLfKO1chQEPfxeu35rowpxjAwYajSq2C4YsJAJOSLe2MhQ5NF0NS6UKcAY",
  },
};

const actionBase = axios.create({ baseURL: baseURL });

export const get = (
  url: any,
  dispatch: Function,
  param = null,
  showResponseMessage = false
) => {
  return new Promise((resolve, reject) => {
    const fullUrl = getFullStringUrl(url, param);
    actionBase
      .get(fullUrl, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};

export const get_by_id = (
  url: any,
  dispatch: Function,
  id:any,
  showResponseMessage = false
) => {
  return new Promise((resolve, reject) => {
    const fullUrl = `${url}/${id}`;
    actionBase
      .get(fullUrl, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};

export const post = (url:any, dispatch:Function, param:any, showResponseMessage = true) => {
  return new Promise((resolve, reject) => {
    actionBase
      .post(url, param, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};

export const put = (url:any, dispatch:Function, param:any, showResponseMessage = true) => {
  return new Promise((resolve, reject) => {
    actionBase
      .put(url, param, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};

export const put_inline_param = (
  url:any,
  dispatch:Function,
  param:any,
  showResponseMessage = true
) => {
  return new Promise((resolve, reject) => {
    const fullUrl = getFullStringUrl(url, param);

    actionBase
      .put(fullUrl, null, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};

export const deletion = (url:any, dispatch:Function, id:any, showResponseMessage = true) => {
  return new Promise((resolve, reject) => {
    const fullUrl = `${url}/${id}`;
    actionBase
      .delete(fullUrl, setHeaders)
      .then((res) => onSuccess(res, dispatch, resolve, showResponseMessage))
      .catch((err) => onFailure(err, dispatch, reject));
  });
};



const getFullStringUrl = (url:any, param:any) => {
  const entries = param ? Object.entries(param) : null;
  let fullUrl = url;
  entries &&
    entries.map((entry, ind) => {
      if (ind == 0) {
        fullUrl = `${fullUrl}?${`${entry[0]}=${entry[1]}`}`;
      } else {
        fullUrl = `${fullUrl}&${`${entry[0]}=${entry[1]}`}`;
      }
    });
  return fullUrl;
};

const onSuccess = (res:any, dispatch:Function, resolve:any, showResponseMessage:any) => {
  const response = res.data;
  if (response.Status == true) {
    if (showResponseMessage) {
      dispatchError(dispatch, response);
    }
    resolve(response);
  } else if (response.Status == undefined) {
    if (res.status == 200) {
      resolve(response);
    } else {
      dispatchError(dispatch, "Response status is not 200");
    }
  } else {
    dispatchError(dispatch, response);
  }
};

const onFailure = (err:any, dispatch:Function, reject:any) => {
  dispatchError(dispatch, err.message);
  // reject(err);
};
