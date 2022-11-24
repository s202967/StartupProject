import React, { useRef, useState } from "react";
import { useDispatch } from "react-redux";
import { Link, NavLink, useHistory } from "react-router-dom";

import { Form, Input, Button } from "element-react";
import { login } from "store/actions/auth";
import Routes from "routes";

const Login = () => {
  const [state, setState] = useState({ username: "", password: "" });
  const rules = {
    username: [
      {
        required: true,
        message: "Please input user name",
        trigger: "blur",
      },
    ],
    password: [
      {
        required: true,
        message: "Please input password",
        trigger: "blur",
      },
    ],
  };
  var formRef: any = useRef();
  const dispatch = useDispatch();
  const history = useHistory();
  const onChange = (key: any, value: any) => {
    setState({ ...state, [key]: value });
  };
  const handleLoginClick = async (e: any) => {
    e.preventDefault();

    formRef.current.validate(async (valid) => {
      if (valid) {
        await dispatch(login(state, history));
      } else {
        return false;
      }
    });
  };
  return (
    <div className="container">
      <div className="login-form">
        <Form
          ref={formRef}
          rules={rules}
          className="demo-form-stacked"
          model={state}
          labelPosition="top"
          labelWidth="100"
        >
          <Form.Item label="Name" prop="username">
            <Input
              value={state.username}
              onChange={(val: any) => onChange("username", val)}
            ></Input>
          </Form.Item>
          <Form.Item label="Password" prop="password">
            <Input
              value={state.password}
              onChange={(val: any) => onChange("password", val)}
            ></Input>
          </Form.Item>
          <Form.Item>
            <Button
              type="primary"
              onClick={handleLoginClick}
              nativeType="submit"
            >
              Login
            </Button>
          </Form.Item>
        </Form>
        <div className="login-form__forgot">
          <span className="forgot-link">Forgot Password?</span>
          <span className="forgot-link">Sign Up</span>
        </div>
      </div>
    </div>
  );
};
export default Login;
