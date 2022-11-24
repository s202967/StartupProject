import React, { useState, useRef } from "react";
import { Form, Input, Button, Layout, Table } from "element-react";
import { AttachmentButton } from "components/form";
import EntryForm from "./entryForm";
const columns = [
  { label: "Name", prop: "FullName", width: 180 },
  { label: "Title", prop: "Title", width: 180 },
  { label: "Designation", prop: "Designation", width: 180 },
  { label: "Institute", prop: "Institute", width: 180 },
  { label: "PostalAddress", prop: "PostalAddress", width: 180 },
  { label: "MobileNo", prop: "MobileNo", width: 180 },
  { label: "Email", prop: "Email", width: 180 },
];

const Authors = (props) => {
  const { onSave } = props;
  const formRef: any = useRef();
  const rules = {};
  const [isVisible, setVisible] = useState(false);
  const [state, setState] = useState({
    FullName: "",
    Title: "",
    Designation: "",
    Institute: "",
    PostalAddress: "",
    MobileNo: "",
    Email: "",
    Photo: "",
    DigitalSignature: "",
    CoAuthors: [],
  });
  const onChange = (name, val) => {
    console.clear();
    console.log(name, val);
    setState({ ...state, [name]: val });
  };
  const handleSubmit = async (e: any) => {
    e.preventDefault();

    formRef.current.validate(async (valid) => {
      if (valid) {
        //await dispatch(GetRoles());
        onSave();
      } else {
        return false;
      }
    });
  };
  const handleReset = (e: any) => {
    formRef.current.resetFields();
    setState({} as any);
  };

  const handleCoAuthorSave = (obj) => {
    let nextState = { ...state, CoAuthors: [...state.CoAuthors] };
    nextState.CoAuthors.push(obj);
    setState(nextState);
    setVisible(false);
  };

  return (
    <div>
      <Form
        ref={formRef}
        rules={rules}
        className="demo-form-stacked"
        model={state}
        labelPosition="top"
        labelWidth="100"
      >
        <Layout.Row gutter="20">
          <Layout.Col span="6">
            <Form.Item label="Title" prop="Title">
              <Input
                value={state.Title}
                onChange={(val: any) => onChange("Title", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            <Form.Item label="Designation" prop="Designation">
              <Input
                value={state.Designation}
                onChange={(val: any) => onChange("Designation", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            <Form.Item label="Full Name" prop="FullName">
              <Input
                value={state.FullName}
                onChange={(val: any) => onChange("FullName", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            <Form.Item label="Email" prop="Email">
              <Input
                value={state.Email}
                onChange={(val: any) => onChange("Email", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            {" "}
            <Form.Item label="Institute" prop="Institute">
              <Input
                value={state.Institute}
                onChange={(val: any) => onChange("Institute", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            {" "}
            <Form.Item label="Address" prop="PostalAddress">
              <Input
                value={state.PostalAddress}
                onChange={(val: any) => onChange("PostalAddress", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6">
            <Form.Item label="Mobile" prop="MobileNo">
              <Input
                value={state.MobileNo}
                onChange={(val: any) => onChange("MobileNo", val)}
              ></Input>
            </Form.Item>
          </Layout.Col>
          <Layout.Col span="6"></Layout.Col>
        </Layout.Row>
        <Layout.Row>
          <Layout.Col span="12">
            <AttachmentButton
              value={state.Photo}
              name="Photo"
              onChange={onChange}
            />
          </Layout.Col>
          <Layout.Col span="12">
            <AttachmentButton
              value={state.DigitalSignature}
              name="DigitalSignature"
              onChange={onChange}
            />
          </Layout.Col>
        </Layout.Row>

        <div>
          <Button onClick={() => setVisible(true)}> Add CoAuthors</Button>
          <Table
            style={{ width: "100%" }}
            columns={columns}
            data={state.CoAuthors}
            fit={true}
          />
        </div>

        <Form.Item>
          <Button type="primary" onClick={handleSubmit} nativeType="submit">
            Save and Continue
          </Button>
        </Form.Item>
      </Form>

      <EntryForm
        isVisible={isVisible}
        setVisible={setVisible}
        onSave={handleCoAuthorSave}
      />
    </div>
  );
};

export default Authors;
