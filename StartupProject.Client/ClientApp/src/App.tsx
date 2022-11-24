import * as React from "react";
import "scss/index.scss";
import "element-theme-default";
import { Provider } from "react-redux";
import configureStore from "store/configureStore";
import Links from "navigation/links";
import Toast from "components/toast";

import { i18n } from "element-react";
import locale from "element-react/src/locale/lang/en";


const store = configureStore({});
i18n.use(locale);

export default () => (
  <Provider store={store}>
    <div className="app">
      <Links />
      <Toast />
    </div>
  </Provider>
);
