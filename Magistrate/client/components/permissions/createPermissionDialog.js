import React from 'react'
import PermissionDialog from './permissionDialog'

var CreatePermissionDialog = React.createClass({

  open() {

    var dialog = this.refs.dialog;
    dialog.resetValues();
    dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;
    var values = dialog.getValue();

    var json = JSON.stringify({
      key: values.key,
      name: values.name,
      description: values.description
    });

    dialog.asyncStart();

    $.ajax({
      url: this.props.url,
      method: "PUT",
      dataType: 'json',
      data: json,
      cache: false,
      success: function(data) {
        dialog.asyncStop();

        if (data) {
          this.props.onCreate(data);
          dialog.close();
        } else {
          this.setState({ keyTaken: true });
        }

      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
      }
    });

  },

  render() {
    return (
      <a className="btn btn-primary" onClick={this.open}>
        Create Permission
        <PermissionDialog onSubmit={this.onSubmit} ref="dialog" acceptText="Create" />
      </a>
    )
  }

});

export default CreatePermissionDialog
