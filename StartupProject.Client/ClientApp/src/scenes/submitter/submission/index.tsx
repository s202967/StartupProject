import React, { useState } from "react";
import { Form, Input, Button, Select, Tabs, Message } from "element-react";
import Start from "./start";
import UploadSubmission from "./uploadSubmission";
import Authors from "./authors";
import MetaData from "./metaData";
import Confirmation from "./confirmation";

const Submisson = (props) => {
  const [steps, setSteps] = useState(5);
  const [selectedTab, setSelectedTab] = useState("1");

  const handleOnSaveStep1 = () => {
    updateStep(2);
    setSelectedTab("2");
  };

  const handleOnSaveStep2 = () => {
    updateStep(3);
    setSelectedTab("3");
  };
  const handleOnSaveStep3 = () => {
    updateStep(4);
    setSelectedTab("4");
  };
  const handleOnSaveStep4 = () => {
    updateStep(5);
    setSelectedTab("5");
  };
  const handleConfirmation = () => {
    Message({
      showClose: true,
      message: "Congrats, submission completed.",
      type: "success",
    });
  };
  const updateStep = (newStep: number) => {
    if (newStep > steps) {
      setSteps(newStep);
    }
  };

  const isDisabled = (tab) => {
    return steps < tab;
  };
  const handleTabClicked = (e) => {
    const { name } = e.props;
    setSelectedTab(name);
  };

  const tabs = [
    {
      label: "1.start",
      name: "1",
      component: <Start onSave={() => handleOnSaveStep1()} />,
    },
    {
      label: "2. Authors and CoAuthors",
      name: "2",
      component: <Authors onSave={() => handleOnSaveStep2()} />,
    },
    {
      label: "3. Upload Submission",
      name: "3",
      component: <UploadSubmission onSave={() => handleOnSaveStep3()} />,
    },
    {
      label: "4. Enter Metadata",
      name: "4",
      component: <MetaData onSave={() => handleOnSaveStep4()} />,
    },
    {
      label: "5.Confirmation",
      name: "5",
      component: <Confirmation onConfirmation={() => handleConfirmation()} />,
    },
  ];

  return (
    <div className="submission">
      <Tabs type="card" value={selectedTab} onTabClick={handleTabClicked}>
        {tabs.map((item, index) => (
          <Tabs.Pane
            key={index}
            label={item.label}
            name={item.name}
            disabled={isDisabled(index + 1)}
          >
            {item.component}
          </Tabs.Pane>
        ))}
      </Tabs>
    </div>
  );
};
export default Submisson;
