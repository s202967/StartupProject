import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Dropdown } from "element-react";
import logo from "../../assets/logo.png";
import { useHistory } from "react-router";
import { logout } from "store/actions/auth";
import { GetLoggedInUserDetails } from "store/actions/user";
import {
  GetCheckList,
  GetSection,
  GetTemplates,
  GetComponents,
} from "store/actions/meta";
import { UserDetailInterface } from "interfaces/actions/auth";

export default function Header(props: any) {
  const history = useHistory();
  let authToken = localStorage.getItem("AuthToken");
  const dispatch = useDispatch();
  const { Email, FullName, UserName } = useSelector(
    (state: any) => state.userReducer.loggedInUserDetail as UserDetailInterface
  );

  useEffect(() => {
    dispatch(GetLoggedInUserDetails());
    dispatch(GetCheckList());
    dispatch(GetSection());
    dispatch(GetTemplates());
    dispatch(GetComponents());
  }, [authToken]);

  const handleCommand = (command) => {
    if (command === "logout") {
      handleLogout();
    }
  };

  const handleLogout = () => {
    dispatch(logout({}, history));
  };

  return (
    <div className="app-header">
      <div className="app-header-logo">
        <img src={logo} />
        <h2>BB Hospital</h2>
      </div>
      <div>
        <Dropdown
          onCommand={handleCommand}
          menu={
            <Dropdown.Menu>
              <Dropdown.Item command="logout"> Log out</Dropdown.Item>
              <Dropdown.Item divided>Info</Dropdown.Item>
            </Dropdown.Menu>
          }
        >
          <span className="el-dropdown-link">
            Welocme {FullName}
            <i className="el-icon-caret-bottom el-icon--right"></i>
          </span>
        </Dropdown>
      </div>
    </div>
  );
}
