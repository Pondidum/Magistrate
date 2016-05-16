const roles = (state = [], action) => {

  switch (action.type) {

    case "UPDATE_ROLES":
      return action.roles;

    case "DELETE_ROLE":
      return state.filter(r => r.key != action.key);

    default:
      return state;
  }
}

export default roles;
