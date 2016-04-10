import React, { Component } from 'react'
import { connect } from 'react-redux'
import Dialog from '../Dialog'
import { deleteUser } from '../../actions'

const mapDispatchToProps = (dispatch) => {
  return {
    deleteUser: (key) => dispatch(deleteUser(key))
  }
}

class DeleteUserDialog extends Component {

  open() {
    this.refs.dialog.open();
  }

  render() {
    const { user, deleteUser } = this.props;

    return (
      <Dialog
        ref="dialog"
        acceptText="Delete"
        acceptStyle="danger"
        title="Delete User"
        onSubmit={() => deleteUser(user.key)}
      >
        <p>Delete user <strong>{user.name}</strong>?</p>
      </Dialog>
    );
  }
}

export default connect(null, mapDispatchToProps, null, {withRef: true})(DeleteUserDialog)
