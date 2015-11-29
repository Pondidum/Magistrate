var AddRolesDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var noSelection = this.props.noSelection;

    return (
      <a href="#" className={"btn btn-raised btn-default" + (noSelection ? " disabled" : "")} onClick={this.showDialog}>
        Add Role
        <Dialog title="Select Roles" onSubmit={() => console.log("rolesDialog.submit") } ref="dialog">
          <p>List of all Roles</p>
        </Dialog>
      </a>
    );
  }

});
