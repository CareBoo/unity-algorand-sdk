#!/bin/zsh

# Define the source and target directories
SOURCE_DIR="libsodium"
TARGET_DIR="libsodium_flat"

# Remove the target directory if it exists
rm -rf $TARGET_DIR

# Create the target directory
mkdir $TARGET_DIR

# Find all .c and .h files in the source directory and copy them to the target directory
find $SOURCE_DIR -type f \( -name "*.c" -o -name "*.h" \) | while read file; do
    cp "$file" "$TARGET_DIR"
done

echo "Flattening complete!"

