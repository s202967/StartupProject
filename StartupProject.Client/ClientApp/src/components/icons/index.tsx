import React from "react";
import PropTypes from "prop-types";
import {
  MdArrowForward,
  MdAdd,
  MdEdit,
  MdDelete,
  MdCheck,
  MdClose,
  MdCheckCircle,
  MdPauseCircleFilled,
  MdPause,
  MdPlayArrow,
  MdFavoriteBorder,
  MdFavorite,
  MdKeyboardArrowDown,
  MdKeyboardArrowRight,
  MdKeyboardArrowLeft,
  MdInfo,
} from "react-icons/md";
import {
  FaUserFriends,
  FaUserEdit,
  FaEllipsisH,
  FaRegCopy,
  FaUmbrellaBeach,
  FaFileInvoice,
  FaMinusCircle,
  FaMinus,
  FaRegFilePdf,
  FaRegFileImage,
  FaRegFileWord,
  FaCheckCircle,
  FaTimesCircle,
  FaTimes,
  FaChartPie,
  FaUserAlt,
  FaShoppingCart,
  FaChartBar,
  FaCog,
  FaPlusCircle,
  FaCaretDown,
  FaPaperclip,
} from "react-icons/fa";
import {
  AiOutlineSearch,
  AiFillAppstore,
  AiOutlineStar,
  AiFillStar,
  AiOutlineUnorderedList,
  AiOutlineDollar,
  AiOutlineSync,
  AiOutlinePrinter,
  AiFillEye,
  AiOutlineFullscreen,
} from "react-icons/ai";

import { IoIosPlayCircle, IoMdStopwatch } from "react-icons/io";
import {
  FiArrowUpRight,
  FiChevronDown,
  FiFileText,
  FiDownload,
  FiUpload,
  FiAlertTriangle,
} from "react-icons/fi";
import { GoCheck, GoChevronDown, GoComment } from "react-icons/go";
import { MdAccessTime } from "react-icons/md";
import { FcOvertime, FcCancel } from "react-icons/fc";
import {
  BsThreeDots,
  BsArrowsAngleExpand,
  BsArrowsAngleContract,
  BsChevronRight,
  BsChevronLeft,
  BsFileRichtext,
} from "react-icons/bs";
import {
  IoMdArrowDropdownCircle
} from "react-icons/io"
import { GiCancel } from "react-icons/gi";
import { BsLink } from "react-icons/bs";
import { RiLogoutCircleLine, RiLockPasswordLine } from "react-icons/ri";

interface Icons {
  name: string;
  size?: number;
  color?: string;
  className?: string;
  styles?: object;
  justSVG?: boolean;
}

function getIconComponent(name: any) {
  switch (name) {
    case "PaperClip":
      return FaPaperclip;
    case "FaPlusCircle":
      return FaPlusCircle;
    case "CaretDown":
      return FaCaretDown;
    case "Cog":
      return FaCog;
    case "BarChart":
      return FaChartBar;
    case "ShoppingCart":
      return FaShoppingCart;
    case "CircleCaretDown":
      return IoMdArrowDropdownCircle;
    case "UserAlt":
      return FaUserAlt;
    case "PieChart":
      return FaChartPie;
    case "ArrowExpand":
      return BsArrowsAngleExpand;
    case "ArrowCollapse":
      return BsArrowsAngleContract;
    case "AngleRight":
      return BsChevronRight;
    case "AngleLeft":
      return BsChevronLeft;
    case "ArrowForward":
      return MdArrowForward;
    case "Add":
      return MdAdd;
    case "List":
      return AiOutlineUnorderedList;
    case "Edit":
      return MdEdit;
    case "Add":
      return MdAdd;
    case "File":
      return FaFileInvoice;
    case "Delete":
      return MdDelete;
    case "ChevronDown":
      return GoChevronDown;
    case "Grab":
      return AiFillAppstore;
    case "FavBorder":
      return AiOutlineStar;
    case "Fav":
      return AiFillStar;
    case "Search":
      return AiOutlineSearch;
    case "Check":
      return MdCheck;
    case "Close":
      return MdClose;
    case "StopWatch":
      return IoMdStopwatch;
    case "BoldCheck":
      return GoCheck;
    case "Play":
      return MdPlayArrow;
    case "Pause":
      return MdPause;
    case "Copy":
      return FaRegCopy;
    //#region Circle
    case "CircleCheck":
      return MdCheckCircle;
    case "CirclePlay":
      return IoIosPlayCircle;
    case "CirclePause":
      return MdPauseCircleFilled;
    case "CircleMinus":
      return FaMinusCircle;
    case "Minus":
      return FaMinus;
    case "ArrowUpRight":
      return FiArrowUpRight;
    case "ArrowDown":
      return MdKeyboardArrowDown;
    case "ArrowLeft":
      return MdKeyboardArrowLeft;
    case "ArrowRight":
      return MdKeyboardArrowRight;
    case "Users":
      return FaUserFriends;
    case "UserEdit":
      return FaUserEdit;
    case "Ellipse":
      return BsThreeDots;
    case "Leave":
      return FaUmbrellaBeach;
    case "Time":
      return MdAccessTime;
    case "Overtime":
      return FcOvertime;
    case "Allowance":
      return AiOutlineDollar;
    case "Comment":
      return GoComment;
    case "Pdf":
      return FaRegFilePdf;
    case "Info":
      return MdInfo;
    case "Image":
      return FaRegFileImage;
    case "Doc":
      return FaRegFileWord;
    case "File":
      return FiFileText;
    case "Approve":
      return FaCheckCircle;
    case "Reject":
      return FcCancel;
    case "Cancel":
      return GiCancel;
    case "Link":
      return BsLink;
    case "Cross":
      return FaTimes;
    case "CrossCircle":
      return FaTimesCircle;
    case "Download":
      return FiDownload;
    case "Upload":
      return FiUpload;
    case "AlertTriangle":
      return FiAlertTriangle;
    case "Sync":
      return AiOutlineSync;
    case "Review":
      return BsFileRichtext;
    case "Print":
      return AiOutlinePrinter;
    case "Eye":
      return AiFillEye;
    case "LogoutCircle":
      return RiLogoutCircleLine;
    //#endregion
    case "LockPassword":
      return RiLockPasswordLine;
    case "FullScreen":
      return AiOutlineFullscreen;

    default:
      return null;
  }
}

function index(props: Icons) {
  const { name, size, color, className, styles, justSVG, ...rest } = props;

  let ReactIcon: any = getIconComponent(name);

  const finalStyles = {
    display: "flex",
    alignItems: "center",
    fontSize: size,
    color: color,
    ...styles,
  };

  if (justSVG) {
    return <ReactIcon />;
  }
  return (
    <span style={finalStyles} className={className} {...rest}>
      <ReactIcon />
    </span>
  );
}

index.defaultProps = {
  name: "",
  size: 20,
  color: "",
  className: "",
  styles: "",
};

export default index;
