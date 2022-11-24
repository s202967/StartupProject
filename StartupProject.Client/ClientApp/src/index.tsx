import * as React from "react";
import * as ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
// import reportWebVitals from "./reportWebVitals";
const baseUrl = document
  .getElementsByTagName("base")[0]
  .getAttribute("href") as string;

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App />
  </BrowserRouter>,
  document.getElementById("root")
);
