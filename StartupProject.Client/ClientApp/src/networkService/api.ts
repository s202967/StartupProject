export const api = {
  auth: {
    siginIn: "auth/signin",
    signOut: "auth/signout",
  },
  meta: {
    templates: "meta/templates",
    checkList: "meta/checklist",
    sections: "meta/sections",
    components: "meta/components",
  },
  common: {
    roles: "security/roles",
  },
  users: {
    list: "security/users/list",
    users: "security/users",
    userDetails: "security/users/details",
    forgotPassword: "security/users/forgot-password",
    resetPassword: "security/users/reset-password",
    changeOwnPassword: "security/users/change-password",
    changeUserPassword: "security/users/change-user-password",
    status: "security/users/status",
  },
};
