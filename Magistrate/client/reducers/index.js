import { combineReducers } from 'redux'

import ui from './ui'
import users from './users'
import roles from './roles'
import permissions from './permissions'

import userValidation from './userValidation'

export default combineReducers({
  ui,
  users,
  roles,
  permissions,
  userValidation
});
