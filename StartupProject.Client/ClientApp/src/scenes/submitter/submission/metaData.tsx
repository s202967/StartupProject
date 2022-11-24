import React, { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Form, Input, Button, Select, Checkbox } from "element-react";
import { CheckList } from "components/form";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";

const MetaData = (props) => {
  var formRef: any = useRef();
  const { onSave } = props;
  const [state, setState] = useState({ Prefix: "", CheckList: [] });
  const [ckBody, setCkBody] = useState("");
  const rules = {};
  const onChange = (key: any, value: any) => {
    setState({ ...state, [key]: value });
  };

  return (
    <div className="user-form">
      <Form
        ref={formRef}
        rules={rules}
        className="demo-form-stacked"
        model={state}
        labelPosition="top"
        labelWidth="100"
      >
        <Form.Item
          label="Acknowledge the copyright statements"
          prop="SectionId"
        >
          <div>
            <Input
              type="textarea"
              value={state.Prefix}
              onChange={(e) => {
                console.log(e);
              }}
            ></Input>
          </div>
        </Form.Item>

        <Form.Item label="Introduction" prop="CommentsForEditor">
          <CKEditor
            editor={ClassicEditor}
            data={ckBody}
            onChange={(event, editor) => {
              const data = editor.getData();
              console.log(data);
              setCkBody(data);
            }}
          />
        </Form.Item>
        <Form.Item label="Methods" prop="CommentsForEditor">
          <CKEditor
            editor={ClassicEditor}
            data={ckBody}
            onChange={(event, editor) => {
              const data = editor.getData();
              console.log(data);
              setCkBody(data);
            }}
          />
        </Form.Item>
        <Form.Item label="Cited References" prop="CommentsForEditor">
          <CKEditor
            editor={ClassicEditor}
            data={ckBody}
            onChange={(event, editor) => {
              const data = editor.getData();
              console.log(data);
              setCkBody(data);
            }}
          />
        </Form.Item>
        <Form.Item>
          <Button type="primary" onClick={onSave}>
            Save and Continue
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};
export default MetaData;
