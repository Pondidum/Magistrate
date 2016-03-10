const userValidation = (state = {}, action) => {

  switch (action.type) {

    case "IS_USER_VALID_RESPONSE":
      return {
        isValid: action.isValid
      };

    default:
      return state;
  }
}

export default userValidation;
