const history = (state = [], action) => {

  switch (action.type) {
    case "UPDATE_HISTORY":
      return action.history;

    default:
      return state;
  }

}

export default history
