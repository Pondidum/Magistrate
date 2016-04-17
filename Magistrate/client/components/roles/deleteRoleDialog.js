import React, { Component } from 'react'
import { connect } from 'react-redux'
import Dialog from '../Dialog'
import { deleteRole } from '../../actions'

const mapDispatchToProps = (dispatch) => {
  return {
    deleteRole: (key) => dispatch(deleteRole(key))
  }
}

class DeleteRoleDialog extends Component {

  open() {
    this.refs.dialog.open();
  }

  render() {
    const { role, deleteRole } = this.props;

    return (
      <Dialog
        ref="dialog"
        acceptText="Delete"
        acceptStyle="danger"
        title="Delete Role"
        onSubmit={() => deleteRole(role.key)}
      >
        <p>Delete role <strong>{role.name}</strong>?</p>
      </Dialog>
    )
  }
}

export default connect(null, mapDispatchToProps, null, {withRef: true})(DeleteRoleDialog)
