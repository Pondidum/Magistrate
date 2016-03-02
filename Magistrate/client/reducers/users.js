const  users = (state = [], action) => {

  switch (action.type) {
    case "CREATE_USER":
      return state.concat([
        {
          id: action.id,
          username: action.username
        }
      ]);

    default:
      return state;
  }

}

export default users;
