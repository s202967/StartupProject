import React, { useEffect, useState } from "react";
import { Route, useHistory } from "react-router";

import Header from "./header";
import Login from "../../scenes/auth/login";
import routes from "routes";

export default function Layout(props: any) {
  let authToken = localStorage.getItem("AuthToken");
  const [state, setState] = useState(authToken);
  let history = useHistory();

  console.log(authToken);
  // useEffect(() => {
  //   console.log("auth token", authToken);
  // }, [authToken]);
  if (!authToken || authToken === "null") {
    history.push(routes.login);
  }
  return authToken ? (
    <>
      <Header />
      <div className="app-container">{props.children}</div>
    </>
  ) : (
    <Route path="/" component={Login} />
  );
}
