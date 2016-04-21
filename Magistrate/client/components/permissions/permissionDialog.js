import React from 'react'
import { Input } from 'react-bootstrap'
import Dialog from '../Dialog'

const PermissionDialog = React.createClass({

  getInitialState() {
    return {
      key: '',
      name: '',
      description: '',
      keyTaken: false,
    };
  },

  resetValues() {
    this.setState(this.getInitialState());
  },

  getValue() {
    return this.state;
  },

  setValue(value) {
    this.setState(value);
  },

  open() {
    this.refs.dialog.open();
  },

  close() {
    this.refs.dialog.close();
  },

  asyncStart() {
    this.refs.dialog.asyncStart();
  },

  asyncStop() {
    this.refs.dialog.asyncStop();
  },

  validateKey() {
    var key = this.state.key;

    if (key == null || key == '' || this.state.keyTaken)
      return 'error';

    return 'success';
  },

  validateName() {
    var name = this.state.name;

    if (name == null || name == '')
      return 'error';

    return 'success';
  },

  render() {

    var acceptText = this.props.acceptText;
    var keyHelp = this.state.keyTaken
      ? "This key is already in use"
      : "Unique identifier for the Permission";

    return (
      <Dialog title={acceptText + " Permission"} onSubmit={this.props.onSubmit} acceptText={acceptText} ref="dialog">
        <form>
          <Input
            type="text"
            value={this.state.key}
            placeholder="e.g. 'some-permission'"
            label="Key"
            help={keyHelp}
            bsStyle={this.validateKey()}
            hasFeedback
            autoFocus
            ref="key"
            onChange={() => { this.setState({ key: this.refs.key.getValue() }); }}
            disabled={this.props.disableKey}/>
          <Input
            type="text"
            value={this.state.name}
            placeholder="Some Permission"
            label="Name"
            help="Name of the permission"
            bsStyle={this.validateName()}
            hasFeedback
            ref="name"
            onChange={() => { this.setState({ name: this.refs.name.getValue() }); }} />
          <Input
            type="textarea"
            value={this.state.description}
            placeholder="Allows users to ..."
            label="Description"
            help="A description of the permission"
            ref="description"
            onChange={() => { this.setState({ description: this.refs.description.getValue() }); }} />
        </form>
      </Dialog>
    );
  }
});

export default PermissionDialog
