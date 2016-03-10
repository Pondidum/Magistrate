import { combineReducers } from 'redux'
import users from './users'
import roles from './roles'
import permissions from './permissions'

import userValidation from './userValidation'

export default combineReducers({
  users,
  roles,
  permissions,
  userValidation
});
