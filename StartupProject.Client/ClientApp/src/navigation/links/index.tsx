import React from "react";
import { Route, Switch } from "react-router-dom";
import Layout from "../layout";
import Redirector from "scenes/auth/redirector";
import Login from "scenes/auth/login";
import Submitter from "scenes/submitter";
import routes from "routes";
import AddUser from "scenes/user";
import submission from "scenes/submitter/submission";

const Links = (props: any) => {
  return (
    <Layout>
      <Switch>
        <Route exact path={"/"} component={Redirector} />
        <Route exact path={routes.submitter.dashboard} component={Submitter} />

        <Route
          exact
          path={routes.submitter.submission}
          component={submission}
        />
        <Route path={routes.user.add} component={AddUser} />
        <Route path={routes.login} component={Login} />
      </Switch>
    </Layout>
  );
};
export default Links;
