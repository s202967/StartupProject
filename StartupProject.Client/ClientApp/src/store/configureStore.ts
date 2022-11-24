import { createStore, applyMiddleware, compose } from "redux";
import thunk from "redux-thunk";
import reducers from "./reducers";

export default function configureStore(initialState: any = {}) {
  const production = process.env.NODE_ENV !== "development";

  let middleware = [thunk];

  // if (!production) {
  //   middleware.push(require("redux-immutable-state-invariant").default());
  // }

  if (production) {
    return createStore(reducers, initialState, applyMiddleware(...middleware));
  } else {
    return createStore(
      reducers,
      initialState,
      compose(applyMiddleware(...middleware))
    );
  }
}
