import React, { Component } from 'react'
import { connect } from 'react-redux'
import Dialog from '../Dialog'
import { deletePermission } from '../../actions'

const mapDispatchToProps = (dispatch) => {
  return {
    deletePermission: (key) => dispatch(deletePermission(key))
  }
}

class DeletePermissionDialog extends Component {

  open() {
    this.refs.dialog.open();
  }

  render() {
    const { permission, deletePermission } = this.props;

    return (
      <Dialog
        ref="dialog"
        acceptText="Delete"
        acceptStyle="danger"
        title="Delete Permission"
        onSubmit={() => deletePermission(permission.key)}
      >
        <p>Delete permission <strong>{permission.name}</strong>?</p>
      </Dialog>
    )
  }
}

export default connect(null, mapDispatchToProps, null, { withRef: true })(DeletePermissionDialog)
