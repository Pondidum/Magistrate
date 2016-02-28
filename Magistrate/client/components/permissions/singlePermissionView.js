import React from 'react'
import InlineEditor from '../InlineEditor'

var SinglePermissionView = React.createClass({

  onNameChanged(newName) {

    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: this.props.url + "/name",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {
        this.props.permission.name = newName;
      }.bind(this)
    });

  },

  onDescriptionChanged(newDescription) {

    var json = JSON.stringify({
      description: newDescription
    });

    $.ajax({
      url: this.props.url + "/description",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {
        this.props.permission.description = newDescription;
      }.bind(this)
    });

  },

  render() {

    var permission = this.props.permission;
    var self = this;

    if (permission == null)
      return (<h1>Unknown permission {this.props.permissionKey}</h1>);

    return (
      <div className="well">

        <h1><InlineEditor initialValue={permission.name} onChange={this.onNameChanged} /></h1>
        <div><InlineEditor initialValue={permission.description} onChange={this.onDescriptionChanged} /></div>

      </div>
    )
  }

});

export default SinglePermissionView
