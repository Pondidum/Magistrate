var AddPermissionsDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var noSelection = this.props.noSelection;

    return (
      <a href="#" className={"btn btn-default" + noSelection} onClick={this.showDialog}>
        Add Permission
        <Dialog title="Select Permissions" onSubmit={() => console.log("permissionsDialog.submit") } ref="dialog">
          <p>List of all Permissions</p>
        </Dialog>
      </a>
    );
  }

});
