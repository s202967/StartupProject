import React, { useState } from "react";
import { Button } from "element-react";
import UploadFile from "./uploadFile";

const UploadSubmission = (props) => {
  const { onSave } = props;
  const [isVisible, setVisible] = useState(false);
  const [files, setFiles] = useState([]);
  const handleSave = (file) => {
    let newFiles = [...files, file];
    console.clear();
    console.log(newFiles);
    setFiles(newFiles);
    setVisible(false);
  };
  return (
    <div className="upload-steps__submissions">
      <div className="upload-steps__submissions-header">
        <div>Submission Files</div>
        <div className="upload-steps__submissions-header-action">
          <div>
            <Button type="text" onClick={() => setVisible(true)}>
              Upload File
            </Button>
          </div>
        </div>
      </div>
      <div className="upload-steps__submissions-body">
        <table>
          {files.map((file, index) => (
            <tr key={index}>
              <td>{file.Attachment.name}</td>
              <td>{file.Componet.Name}</td>
            </tr>
          ))}
        </table>
      </div>
      <UploadFile
        isVisible={isVisible}
        setVisible={setVisible}
        onSave={(obj) => handleSave(obj)}
      />
      <div className="upload-steps__submissions-footer">
        <Button type="primary" onClick={onSave} disabled={files.length === 0}>
          Save and Continue
        </Button>
      </div>
    </div>
  );
};
export default UploadSubmission;
