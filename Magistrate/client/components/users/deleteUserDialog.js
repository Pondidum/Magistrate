import React from 'react'
import { connect } from 'react-redux'
import Dialog from '../Dialog'
import { deleteUser } from '../../actions'

const mapDispatchToProps = (dispatch) => {
  return {
    deleteUser: (key) => dispatch(deleteUser(key))
  }
}

const DeleteUserDialog = ({ user, deleteUser }) => (
  <Dialog
    acceptText="Delete"
    acceptStyle="danger",
    title="Delete User"
    accept={() => deleteUser(user.key)}
  >
    <p>Delete user <strong>{user.name}</strong>?</p>
  </Dialog>
);

export default connect(null, mapDispatchToProps)(DeleteUserDialog)
