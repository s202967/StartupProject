import React, { useState, useRef } from "react";
import { Form, Input, Button, Layout, Dialog } from "element-react";
import { AttachmentButton } from "components/form";

const EntryForm = (props) => {
  const { isVisible, setVisible, onSave } = props;
  const formRef: any = useRef();
  const rules = {};
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
        onSave && onSave(state);
        
        //await dispatch(GetRoles());
      } else {
        return false;
      }
    });
  };
  const handleReset = (e: any) => {
    formRef.current.resetFields();
    setState({} as any);
  };
  return (
    <div>
      <Dialog
        title="Co-Authors"
        visible={isVisible}
        onCancel={() => setVisible(false)}
      >
        <Dialog.Body>
          <Form
            ref={formRef}
            rules={rules}
            className="demo-form-stacked"
            model={state}
            labelPosition="top"
            labelWidth="100"
          >
            <Layout.Row gutter="20">
              <Layout.Col span="12">
                <Form.Item label="Title" prop="Title">
                  <Input
                    value={state.Title}
                    onChange={(val: any) => onChange("Title", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                <Form.Item label="Designation" prop="Designation">
                  <Input
                    value={state.Designation}
                    onChange={(val: any) => onChange("Designation", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                <Form.Item label="Full Name" prop="FullName">
                  <Input
                    value={state.FullName}
                    onChange={(val: any) => onChange("FullName", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                <Form.Item label="Email" prop="Email">
                  <Input
                    value={state.Email}
                    onChange={(val: any) => onChange("Email", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                {" "}
                <Form.Item label="Institute" prop="Institute">
                  <Input
                    value={state.Institute}
                    onChange={(val: any) => onChange("Institute", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                {" "}
                <Form.Item label="Address" prop="PostalAddress">
                  <Input
                    value={state.PostalAddress}
                    onChange={(val: any) => onChange("PostalAddress", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12">
                <Form.Item label="Mobile" prop="MobileNo">
                  <Input
                    value={state.MobileNo}
                    onChange={(val: any) => onChange("MobileNo", val)}
                  ></Input>
                </Form.Item>
              </Layout.Col>
              <Layout.Col span="12"></Layout.Col>
            </Layout.Row>
            <Layout.Row>
              <Layout.Col span="12">
                {" "}
                <AttachmentButton
                  value={state.Photo}
                  name="Photo"
                  onChange={onChange}
                />
              </Layout.Col>
              <Layout.Col span="12">
                {" "}
                <AttachmentButton
                  value={state.DigitalSignature}
                  name="DigitalSignature"
                  onChange={onChange}
                />
              </Layout.Col>
            </Layout.Row>
            <Form.Item>
              <Button type="primary" onClick={handleSubmit} nativeType="submit">
                Save
              </Button>
              <Button onClick={handleReset}>Reset</Button>
            </Form.Item>
          </Form>
        </Dialog.Body>
      </Dialog>
    </div>
  );
};

export default EntryForm;
