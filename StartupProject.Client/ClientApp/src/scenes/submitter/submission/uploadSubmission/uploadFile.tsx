import React, { useRef, useState } from "react";
import { Dialog, Form, Input, Select, Button } from "element-react";
import { AttachmentButton } from "components/form";
import { useSelector } from "react-redux";
const UploadFile = (props) => {
  const { isVisible, setVisible, onSave } = props;
  const [state, setState] = useState({ Componet: "", Attachment: {} });
  const { components } = useSelector((state: any) => state.metaReducer);

  const handleChange = (name, value) => {
    setState({ ...state, [name]: value });
  };
  const handleSave = () => {
    onSave && onSave(state);
    setState({ Componet: "", Attachment: {} });
  };
  return (
    <Dialog
      title="Shipping Address"
      visible={isVisible}
      onCancel={() => setVisible(false)}
    >
      <Dialog.Body>
        <Form model={state} labelPosition="top">
          <Form.Item label="Article Component">
            <Select
              value={state.Componet}
              placeholder="Please select component"
              onChange={(val) => handleChange("Componet", val)}
            >
              {components &&
                components.map((item, index) => (
                  <Select.Option
                    key={index}
                    label={item.Name}
                    value={item}
                  ></Select.Option>
                ))}
            </Select>
          </Form.Item>
          <Form.Item label="Attachment">
            <AttachmentButton
              value={state.Attachment}
              name="Attachment"
              onChange={handleChange}
            />
          </Form.Item>
          <Form.Item>
            <Button type="primary" onClick={() => handleSave()}>
              Save
            </Button>
          </Form.Item>
        </Form>
      </Dialog.Body>
    </Dialog>
  );
};
export default UploadFile;
