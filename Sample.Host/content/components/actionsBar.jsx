var ActionsBar = React.createClass({

  getInitialState() {
    return {
      permissionsShown: false
    };
  },

  render() {

    var disabled = this.props.hasSelection
      ? ""
      : " disabled";

    return (
      <div className={this.props.className}>
        <ul className="list-unstyled list-inline">
          <li><a href="#" className="btn btn-primary">Create User</a></li>
          <li><a href="#" className={"btn btn-default" + disabled} onClick={() => this.refs.permissionsDialog.open()}>Add Permission</a></li>
          <li><a href="#" className={"btn btn-default" + disabled}>Add Role</a></li>
        </ul>
        <Dialog title="Select Permissions" onSubmit={() => console.log("permissionsDialog.submit") } ref="permissionsDialog">
          <p>List of all Permissions</p>
        </Dialog>

      </div>
    );
  }

});
