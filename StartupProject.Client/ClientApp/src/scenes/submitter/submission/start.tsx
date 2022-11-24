import React, { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Form, Input, Button, Select, Checkbox } from "element-react";
import { CheckList } from "components/form";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";

const Start = (props) => {
  var formRef: any = useRef();
  const { onSave } = props;
  const [state, setState] = useState({ SectionId: "", CheckList: [] });
  const [ckBody, setCkBody] = useState("");
  const rules = {};
  const { checkList, templates, sections } = useSelector((state: any) => {
    console.log(state);
    return state.metaReducer;
  });
  const onChange = (key: any, value: any) => {
    setState({ ...state, [key]: value });
  };

  const copyrightStatements = templates.find(
    (x) => x.TemplateKey === "copyrightStatements"
  );

  const privacyStatements = templates.find(
    (x) => x.TemplateKey === "privacyStatements"
  );

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
          label={copyrightStatements && copyrightStatements.TemplateTitle}
          prop="SectionId"
        >
          <div>{copyrightStatements && copyrightStatements.TemplateText}</div>
        </Form.Item>
        <Form.Item label="Section" prop="SectionId">
          <Select
            value={state.SectionId}
            placeholder="Section"
            onChange={(val: any) => onChange("SectionId", val)}
          >
            {sections &&
              sections.map((role) => (
                <Select.Option
                  label={role.Name}
                  value={role.Id}
                ></Select.Option>
              ))}
          </Select>
        </Form.Item>
        <Form.Item label="CheckList" prop="CheckList">
          <CheckList
            list={checkList}
            value={state.CheckList}
            onChange={(val: any) => onChange("CheckList", val)}
          />
        </Form.Item>
        <Form.Item label="Comments for the Editor" prop="CommentsForEditor">
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
        <Form.Item
          label={privacyStatements && privacyStatements.TemplateTitle}
          prop="privacyStatements"
        >
          <div>{privacyStatements && privacyStatements.TemplateText}</div>
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
export default Start;
