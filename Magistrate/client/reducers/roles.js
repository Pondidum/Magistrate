const roles = (state = [], action) => {

  switch (action.type) {

    case "COLLECTIONS_CHANGED":
      return action.roles;

    case "DELETE_ROLE":
      return state.filter(r => r.key != action.key);

    default:
      return state;
  }
}

export default roles;
