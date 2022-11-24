import React from "react";
import { Button } from "element-react";
const Confirmation = (props) => {
  return (
    <div>
      <div>
        Your submission has been uploaded and is ready to be sent. You may go
        back to review and adjust any of the information you have entered before
        continuing. When you are ready, click "Finish Submission"
      </div>
      <div>
        <Button type="primary" onClick={() => props.onConfirmation()}>
          Finish Submission
        </Button>
      </div>
    </div>
  );
};

export default Confirmation;
