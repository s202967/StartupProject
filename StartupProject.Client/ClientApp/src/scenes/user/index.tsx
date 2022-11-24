import React, { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, NavLink, useHistory } from "react-router-dom";

import { Form, Input, Button, Select } from "element-react";
import Routes from "routes";
import { AddUserInterface } from "interfaces/user";
import { GetRoles } from "store/actions/common";

const AddUser = () => {
  const [state, setState] = useState({} as AddUserInterface);
  const { roles } = useSelector((state: any) => state.commonReducer);
  const rules = {
    FullName: [
      {
        required: true,
        message: "Full name is required",
        trigger: "blur",
      },
    ],
    Email: [
      {
        required: true,
        message: "Email is required",
        trigger: "blur",
      },
      {
        type: "email",
        message: "Please input correct email address",
        trigger: "blur,change",
      },
    ],
    RoleId: [
      {
        required: true,
        message: "Role is required",
        trigger: "blur",
      },
    ],
  };
  var formRef: any = useRef();
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    dispatch(GetRoles());
  }, []);

  const onChange = (key: any, value: any) => {
    console.log(key, value);
    setState({ ...state, [key]: value });
  };
  const handleSubmit = async (e: any) => {
    e.preventDefault();

    formRef.current.validate(async (valid) => {
      if (valid) {
        //await dispatch(GetRoles());
      } else {
        return false;
      }
    });
  };
  const handleReset = (e: any) => {
    formRef.current.resetFields();
    setState({} as AddUserInterface);
  };

  return (
    <div className="container">
      <div className="user-form">
        <Form
          ref={formRef}
          rules={rules}
          className="demo-form-stacked"
          model={state}
          labelPosition="top"
          labelWidth="100"
        >
          <Form.Item label="Full Name" prop="FullName">
            <Input
              value={state.FullName}
              onChange={(val: any) => onChange("FullName", val)}
            ></Input>
          </Form.Item>
          <Form.Item label="Email" prop="Email">
            <Input
              value={state.Email}
              onChange={(val: any) => onChange("Email", val)}
            ></Input>
          </Form.Item>

          <Form.Item label="Mobile" prop="MobileNumber">
            <Input
              value={state.MobileNumber}
              onChange={(val: any) => onChange("MobileNumber", val)}
            ></Input>
          </Form.Item>

          <Form.Item label="Country" prop="Country">
            <Input
              value={state.Country}
              onChange={(val: any) => onChange("Country", val)}
            ></Input>
          </Form.Item>

          <Form.Item label="Affilation" prop="Affilation">
            <Input
              value={state.Affilation}
              onChange={(val: any) => onChange("Affilation", val)}
            ></Input>
          </Form.Item>

          <Form.Item label="Role" prop="RoleId">
            <Select
              value={state.RoleId}
              placeholder="Role"
              onChange={(val: any) => onChange("RoleId", val)}
            >
              {roles &&
                roles.map((role) => (
                  <Select.Option
                    label={role.Name}
                    value={role.Id}
                  ></Select.Option>
                ))}
            </Select>
          </Form.Item>

          <Form.Item>
            <Button type="primary" onClick={handleSubmit} nativeType="submit">
              Save
            </Button>
            <Button onClick={handleReset}>Reset</Button>
          </Form.Item>
        </Form>
      </div>
    </div>
  );
};
export default AddUser;
