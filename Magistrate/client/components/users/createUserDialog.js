import React from 'react'
import { connect } from 'react-redux'
import { Input } from 'react-bootstrap'
import Dialog from '../dialog'

import { createUser, validateUser } from '../../actions'

const mapStateToProps = (state) => {
  return {
    users: state.users,
    isValid: state.userValidation.isValid
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    createUser: (key, username) => dispatch(createUser(key, username)),
    validateUser: (key) => dispatch(validateUser(key))
  }
}


var CreateUserDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {

    this.props.createUser(this.state.key, this.state.name);
    this.refs.dialog.close();

  },

  getInitialState() {
    return {
      key: '',
      name: ''
    };
  },

  validateKey() {
    var key = this.state.key;

    if (key == null || key == '' || !this.props.isValid)
      return 'error';

    return 'success';
  },

  onKeyChanged() {
    var key = this.refs.key.getValue();
    this.setState({ key: key });
    this.props.validateUser(key);
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

    var keyHelp = !this.props.isValid
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

export default connect(mapStateToProps, mapDispatchToProps)(CreateUserDialog)
