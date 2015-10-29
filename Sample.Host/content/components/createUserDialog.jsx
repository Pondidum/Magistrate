var Input = ReactBootstrap.Input;

var CreateUserDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {
    console.log("create.submit");
  },

  getInitialState() {
    return {
      key: '',
      name: ''
    };
  },

  validateKey() {
    let length = this.state.key.length;
    if (length > 10) return 'success';
    else if (length > 5) return 'warning';
    else if (length > 0) return 'error';
  },

  onKeyChanged() {
    this.setState({
      key: this.refs.key.getValue()
    });
  },

  validateName() {
    let length = this.state.name.length;
    if (length > 10) return 'success';
    else if (length > 5) return 'warning';
    else if (length > 0) return 'error';
  },

  onNameChanged() {
    this.setState({
      name: this.refs.name.getValue()
    });
  },

  render() {
    return (
      <a href="#" className="btn btn-primary" onClick={this.showDialog}>
        Create User
        <Dialog title="Create User" onSubmit={this.onSubmit} ref="dialog">
          <Input
            type="text"
            value={this.state.key}
            placeholder="e.g. 'BD659BC8-D5CE-43BC-A581-D41C534A3BE6'"
            label="Key"
            help="Unique identifier for the User"
            bsStyle={this.validateKey()}
            hasFeedback
            standalone
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
            standalone
            ref="name"
            onChange={this.onNameChanged} />
        </Dialog>
      </a>
    );
  }

});
