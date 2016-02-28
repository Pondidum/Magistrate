import React from 'react'
import { Input } from 'react-bootstrap'
import Dialog from '../dialog'

var CreateUserDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;

    dialog.asyncStart();

    var json = JSON.stringify({
      key: this.state.key,
      name: this.state.name
    });

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

  getInitialState() {
    return {
      key: '',
      name: '',
      keyTaken: false,
    };
  },

  validateKey() {
    var key = this.state.key;

    if (key == null || key == '' || this.state.keyTaken)
      return 'error';

    return 'success';
  },

  onKeyChanged() {
    this.setState({
      key: this.refs.key.getValue()
    });
  },

  validateName() {
    var name = this.state.name;

    if (name == null || name == '')
      return 'error';

    return 'success';
  },

  onNameChanged() {
    this.setState({
      name: this.refs.name.getValue()
    });
  },

  render() {

    var keyHelp = this.state.keyTaken
      ? "This key is already in use"
      : "Unique identifier for the User";

    return (
      <a href="#" className="btn btn-raised btn-primary" onClick={this.showDialog}>
        Create User
        <Dialog title="Create User" onSubmit={this.onSubmit} acceptText="Create" ref="dialog">
          <form>
            <Input
              type="text"
              value={this.state.key}
              placeholder="e.g. 'BD659BC8-D5CE-43BC-A581-D41C534A3BE6'"
              label="Key"
              help={keyHelp}
              bsStyle={this.validateKey()}
              hasFeedback
              autoFocus
              ref="key"
              onChange={this.onKeyChanged} />
            <Input
              type="text"
              value={this.state.name}
              placeholder="Andy Dote"
              label="Name"
              help="Name of the user"
              bsStyle={this.validateName()}
              hasFeedback
              ref="name"
              onChange={this.onNameChanged} />
          </form>
        </Dialog>
      </a>
    );
  }

});
